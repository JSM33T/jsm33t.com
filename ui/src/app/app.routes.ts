import { Routes } from '@angular/router';

export const routes: Routes = [
	{
		path: '',
		loadComponent: () => import('./modules/home/home').then(m => m.Home)
	},
	{
		path: 'about',
		loadComponent: () => import('./modules/about/about').then(m => m.About)
	}
];
