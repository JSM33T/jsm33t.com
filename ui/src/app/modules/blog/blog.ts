import { Component, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpApiService } from '../../services/httpapi-service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { OffcanvasService } from '../../services/offcanvas-service';
import { FormsModule } from '@angular/forms';

interface BlogFilterRequest {
	fromDate: string;
	toDate: string;
	categoryId: number;
	tag: string;
	page: number;
	pageSize: number;
}

@Component({
	selector: 'app-blog',
	imports: [CommonModule, RouterLink, FormsModule],
	templateUrl: './blog.html',
	styleUrl: './blog.css'
})
export class Blog {
	apiUrl: string = environment.apiUrl;
	blogs: any[] = [];
	categories: any[] = [];
	selectedCategoryId: number | null = null;
	isSideBarOpen: boolean = false;
	isLoading: boolean = true; // ✅ added
	offcanvasService = inject(OffcanvasService);

	years: number[] = [2023, 2024, 2025, 2026, 2027, 2028];
	selectedYear: number | null = null;

	// Pagination related variables
	currentPage: number = 1;
	pageSize: number = 10;
	totalPages: number = 1;

	constructor(
		private httpApiService: HttpApiService,
		private route: ActivatedRoute,
		private router: Router
	) { }

	ngOnInit(): void {
		this.loadCategories();

		this.route.queryParams.subscribe(params => {
			this.selectedCategoryId = params['categoryId'] !== undefined ? +params['categoryId'] : null;
			this.selectedYear = params['year'] ? +params['year'] : null;

			this.currentPage = +params['page'] || 1;
			this.pageSize = +params['pageSize'] || 10;

			let fromDate: string;
			let toDate: string;

			if (this.selectedYear) {
				fromDate = `${this.selectedYear}-01-01T00:00:00Z`;
				toDate = `${this.selectedYear}-12-31T23:59:59Z`;
			} else {
				fromDate = '1970-01-01T00:00:00Z';
				toDate = '2100-12-31T23:59:59Z';
			}

			const request: BlogFilterRequest = {
				fromDate: fromDate,
				toDate: toDate,
				categoryId: this.selectedCategoryId || 0,
				tag: params['tag'] || '',
				page: this.currentPage,
				pageSize: this.pageSize
			};

			this.fetchBlogs(request);
		});
	}

	loadCategories(): void {
		this.httpApiService.get<any>(`${this.apiUrl}/blog/categories`)
			.subscribe(res => {
				this.categories = res.data || [];
			});
	}

	fetchBlogs(request: BlogFilterRequest): void {
		this.isLoading = true; // ✅ set loading before request
		this.httpApiService.post<any>(`${this.apiUrl}/blog/filter`, request)
			.subscribe(res => {
				this.isLoading = false; // ✅ stop loading after response
				this.blogs = res.data.items || [];
				this.totalPages = res.data.totalPages || 100;
			}, err => {
				this.isLoading = false; // ✅ stop loading on error
				console.error('API error:', err);
				this.blogs = [];
			});
	}
	selectCategory(categoryId: number | null): void {
		const queryParams = { ...this.route.snapshot.queryParams };

		if (categoryId !== null) {
			queryParams['categoryId'] = categoryId;
		} else {
			delete queryParams['categoryId'];
		}

		this.router.navigate([], {
			relativeTo: this.route,
			queryParams: queryParams
		});
	}

	selectYear(year: number | null): void {
		const queryParams = { ...this.route.snapshot.queryParams };

		if (year !== null) {
			queryParams['year'] = year;
		} else {
			delete queryParams['year'];
		}

		this.router.navigate([], {
			relativeTo: this.route,
			queryParams: queryParams
		});
	}

	// Pagination functions
	nextPage(): void {
		if (this.currentPage < this.totalPages) {
			this.updatePage(this.currentPage + 1);
		}
	}

	prevPage(): void {
		if (this.currentPage > 1) {
			this.updatePage(this.currentPage - 1);
		}
	}

	updatePage(page: number): void {
		const queryParams = { ...this.route.snapshot.queryParams, page: page, pageSize: this.pageSize };

		this.router.navigate([], {
			relativeTo: this.route,
			queryParams: queryParams
		});
	}

	setPageSize(size: number): void {
		this.pageSize = size;
		this.updatePage(1);
	}

	clearFilters(): void {
		const queryParams = { ...this.route.snapshot.queryParams };

		delete queryParams['categoryId'];
		delete queryParams['year'];

		this.router.navigate([], {
			relativeTo: this.route,
			queryParams: queryParams
		});
	}
}
