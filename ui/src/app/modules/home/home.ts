import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { Meta } from '@angular/platform-browser';

@Component({
	selector: 'app-home',
	imports: [RouterLink],
	templateUrl: './home.html',
	styleUrl: './home.css'
})
export class Home {
	titleText = 'Home';
	descriptionText = 'This is the home page of our application.';

	constructor(private title: Title, private meta: Meta) { }

	ngOnInit(): void {
		this.title.setTitle(this.titleText);
		this.meta.updateTag({ name: 'description', content: this.descriptionText });
	}
}
