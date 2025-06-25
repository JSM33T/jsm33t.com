import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { Meta } from '@angular/platform-browser';

@Component({
	selector: 'app-about',
	imports: [RouterLink],
	templateUrl: './about.html',
	styleUrl: './about.css'
})
export class About {
	titleText = 'About';
	descriptionText = 'This is the about page of our application.';

	constructor(private title: Title, private meta: Meta) { }

	ngOnInit(): void {
		this.title.setTitle(this.titleText);
		this.meta.updateTag({ name: 'description', content: this.descriptionText });
	}
}
