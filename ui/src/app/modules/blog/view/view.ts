import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpApiService } from '../../../services/httpapi-service';
import { environment } from '../../../../environments/environment';


@Component({
	selector: 'app-blog-view',
	imports: [CommonModule, RouterLink],
	templateUrl: './view.html',
	styleUrl: './view.css'
})
export class View {
	apiUrl: string = environment.apiUrl;
	blog: any = null;
	slug: string = '';

	private httpApiService = inject(HttpApiService);
	private route = inject(ActivatedRoute);
	private router = inject(Router);

	ngOnInit(): void {
		this.route.params.subscribe(params => {
			this.slug = params['slug'];
			if (this.slug) {
				this.fetchBlog();
			}
		});
	}

	fetchBlog(): void {
		this.httpApiService.get<any>(`${this.apiUrl}/blog/view/${this.slug}`)
			.subscribe(res => {
				if (res.status === 200) {
					this.blog = res.data;
				} else {
					console.error('Failed to fetch blog:', res.message);
				}
			});
	}
	filterByTag(tag: string): void {
		// Navigate to blog list with tag as query param
		this.router.navigate(['/blogs'], { queryParams: { tag: tag } });
	}
}
