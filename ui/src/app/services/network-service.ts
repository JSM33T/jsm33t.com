import { effect, Injectable, signal } from '@angular/core';

@Injectable({
	providedIn: 'root'
})
export class NetworkService {

	isOnline = signal(navigator.onLine);

	constructor() {
		window.addEventListener('online', this.setOnlineStatus.bind(this));
		window.addEventListener('offline', this.setOnlineStatus.bind(this));

		effect(() => {
			if (!this.isOnline()) {
				// Place your offline logic here
				console.warn('You are offline!');
			}
		});
	}

	private setOnlineStatus() {
		this.isOnline.set(navigator.onLine);
	}
}
