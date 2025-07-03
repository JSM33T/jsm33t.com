import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpApiService } from '../../../services/httpapi-service';
import { environment } from '../../../../environments/environment';
import { marked } from 'marked';
import hljs from 'highlight.js';

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
	isLoading: boolean = true;

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

		const renderer = new marked.Renderer();
		renderer.code = ({ text, lang }) => {
			if (lang && hljs.getLanguage(lang)) {
				const highlighted = hljs.highlight(text, { language: lang }).value;
				return `<pre><code class="hljs ${lang}">${highlighted}</code></pre>`;
			} else {
				return `<pre><code class="hljs">${hljs.highlightAuto(text).value}</code></pre>`;
			}
		};
		marked.use({ renderer });
	}

	fetchBlog(): void {
		this.isLoading = true;
		this.httpApiService.get<any>(`${this.apiUrl}/blog/view/${this.slug}`)
			.subscribe(res => {
				this.isLoading = false;
				if (res.status === 200) {
					this.blog = res.data;
					this.blog.parsedContent = marked.parse(this.blog.content || '');
				} else {
					this.blog = null;
					console.error('Failed to fetch blog:', res.message);
				}
			}, err => {
				this.isLoading = false;
				this.blog = null;
				console.error('API error', err);
			});
	}

	filterByTag(tag: string): void {
		this.router.navigate(['/blogs'], { queryParams: { tag: tag } });
	}
}
