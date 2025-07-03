import { Component, ElementRef, ViewChild } from '@angular/core';
import { gsap } from 'gsap';

@Component({
	selector: 'app-logo',
	imports: [],
	templateUrl: './logo.html',
	styleUrl: './logo.css'
})
export class Logo {
	@ViewChild('svg', { static: false }) svg!: ElementRef;

	ngAfterViewInit(): void {
		if (!this.svg) {
			console.error('SVG ViewChild not found');
			return;
		}

		const paths: NodeListOf<SVGPathElement> = this.svg.nativeElement.querySelectorAll('path');

		// Initial appear animation for each path sequentially
		paths.forEach((path, i) => {
			gsap.set(path, {
				opacity: 0,
				scale: 1,
				x: 0,
				y: 0,
				scaleX: 1,
				scaleY: 1,
				transformOrigin: 'center center',
				filter: 'drop-shadow(0 0 0px #ffd700)'
			});

			gsap.to(path, {
				opacity: 1,
				duration: 0.5,
				delay: i * 0.2,
				ease: 'power3.out'
			});
		});

		// Mousemove scatter + random flip effect
		window.addEventListener('mousemove', (e) => {
			const mouseX = e.clientX;
			const mouseY = e.clientY;

			paths.forEach((path) => {
				const bbox = path.getBoundingClientRect();
				const cx = bbox.left + bbox.width / 2;
				const cy = bbox.top + bbox.height / 2;

				const dx = mouseX - cx;
				const dy = mouseY - cy;
				const distance = Math.sqrt(dx * dx + dy * dy);

				const maxDist = 150;
				const ratio = Math.max(0, 1 - distance / maxDist);

				if (ratio > 0) {
					// बिखरना और घुमाना
					const angle = Math.random() * Math.PI * 2;
					const scatterX = Math.cos(angle) * ratio * 20;
					const scatterY = Math.sin(angle) * ratio * 20;

					// उल्टा घुमाना: आड़ा या सीधा
					const flipHorizontally = Math.random() > 0.5;
					const flipValue = ratio > 0.5 ? -1 : 1;

					const flipProps = flipHorizontally
						? { scaleX: flipValue, scaleY: 1 }
						: { scaleX: 1, scaleY: flipValue };

					gsap.to(path, {
						x: scatterX,
						y: scatterY,
						scale: 1 + ratio * 0.1,
						...flipProps,
						filter: `drop-shadow(0 0 ${10 + ratio * 20}px #ffd700)`,
						duration: 0.4,
						ease: 'power2.out'
					});
				} else {
					// माउस के दूर जाने पर वापस असली जगह पर ले जाना
					gsap.to(path, {
						x: 0,
						y: 0,
						scale: 1,
						scaleX: 1,
						scaleY: 1,
						filter: 'drop-shadow(0 0 0px #ffd700)',
						duration: 0.6,
						ease: 'power2.out'
					});
				}
			});
		});
	}
}
