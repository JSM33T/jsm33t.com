import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Meta } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { sectionsData } from './about.data';
import { RouterLink, RouterModule } from '@angular/router';

@Component({
	selector: 'app-about',
	imports: [CommonModule, RouterModule],
	templateUrl: './about.html',
	styleUrl: './about.css'
})
export class About {
	titleText = 'About';
	descriptionText = 'This is the about page of our application.';
	sections = sectionsData;

	constructor(private title: Title, private meta: Meta) { }

	ngOnInit(): void {
		this.title.setTitle(this.titleText);
		this.meta.updateTag({ name: 'description', content: this.descriptionText });
	}
}
