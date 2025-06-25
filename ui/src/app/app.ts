import { Component, effect, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from "./shared/navbar/navbar";
import { NetworkService } from './services/network-service';
import { CommonModule, NgIf } from '@angular/common';
import { Chatbot } from './components/shared/chatbot/chatbot';


@Component({
	selector: 'app-root',
	imports: [RouterOutlet, Navbar, CommonModule,Chatbot],
	templateUrl: './app.html',
	styleUrl: './app.css'
})
export class App {

    isLoading = signal(true);
	isOnline = inject(NetworkService).isOnline;

	network = inject(NetworkService);

	constructor() {

		setTimeout(() => this.isLoading.set(false), 2000);

		effect(() => {
			if (!this.network.isOnline()) {
				this.isOnline.set(false);
				this.isLoading.set(true);
			} else {
				this.isOnline.set(true);
				this.isLoading.set(false);
			}
		});

	}
}