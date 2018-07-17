/**
 * simplezoom.js
 * Author: Kushagra Gour
 * MIT Licensed
 */
(function () {
	var snapDistance = 30;
	var el, underlay, img, bound, W, H,
		isOutside = false, isOpen, isTouch, hasMoved;

	function init() {
		W = window.innerWidth;
		H = window.innerHeight;
		underlay = document.createElement('div');
		underlay.setAttribute('style', 'position: fixed; left:0; top:0; width:100vw; height: 100vh; transition: opacity 0.25s ease 200ms; opacity: 0; pointer-events:none; background: rgba(0,0,0,0.7);');
		document.body.appendChild(underlay);

		img = document.createElement('img');
		img.setAttribute('style', 'max-width: 98vw; position: fixed; left: 50%; top: 0%; transform: translate(0%, 0%); transition: transform 0.3s ease 200ms, opacity 0.4s ease 200ms; opacity: 0;pointer-events:none;');
		document.body.appendChild(img);

		try {
			// Detect touch screen. Otherwise will throw err.
			document.createEvent('TouchEvent');
			img.style.transform = 'translate(-50%, -50%)';
			img.style.left = '50%';
			img.style.top = '50%';
			img.style.maxWidth = (W) + 'px';

			document.addEventListener('touchstart', onMouseOver);
			isTouch = true;
		} catch(e) {
			document.addEventListener('mouseover', onMouseOver);
		}
	}

	init();

	function onTouchMove(e) {
		hasMoved = true;
	}
	function onTouchEnd(e) {
		document.removeEventListener('touchmove', onTouchMove);
		document.removeEventListener('touchend', onTouchEnd);
		// If it wasn't a swipe, open/close image
		if (!hasMoved) {
			if (isOpen) {
				isOpen = false;
				reset();
			} else if (e.target.tagName === 'IMG') {
				openImage(e.target);
			}
		}
	}

	/**
	 * Checks all 4 sides of target element which has max area available
	 * for the image, maintaining the aspect ratio.
	 * @return {string} Side which has max area.
	 */
	function getSideWithMaxArea() {
		var aspectRatio = el.width / el.height;
		var areas = {
			top: (aspectRatio * bound.top) * bound.top,
			bottom: (aspectRatio * (H - bound.bottom)) * (H - bound.bottom),
			left: (bound.left / aspectRatio) * (bound.left),
			right: ((W - bound.right) / aspectRatio) * (W - bound.right)
		};

		var maxArea = 0, maxSide;
		for (var side in areas) {
			if (areas[side] > maxArea) {
				maxArea = areas[side];
				maxSide = side;
			}
		}
		return maxSide;
	}

	function openImage(target) {
		el = target;
		img.src = el.src;
		img.style.opacity = 1;
		underlay.style.opacity = 1;

		if (isTouch) {
			img.style.transform = 'translate(-50%, -50%) scale(1)';
		}
		else {
			img.style.top = '0px';
			img.style.transform = 'translate(0%, 0%) scale(1)';
			bound = el.getBoundingClientRect();

			var side = getSideWithMaxArea();

			if (side === 'top') {
				img.style.maxHeight = (bound.top) + 'px';
				img.style.maxWidth = 'none';
				img.style.left = 0;
				img.style.top = 0;
			} else if (side === 'bottom') {
				img.style.maxHeight = (H - bound.bottom) + 'px';
				img.style.maxWidth = 'none';
				img.style.left = 0;
				img.style.top = bound.bottom + 'px';
			} else if (side === 'right') {
				img.style.maxWidth = (W - bound.right) + 'px';
				img.style.maxHeight = 'none';
				img.style.left = (bound.right) + 'px';
			} else {
				img.style.maxWidth = (bound.left) + 'px';
				img.style.maxHeight = 'none';
				img.style.left = 0;
			}
			document.addEventListener('mousemove', onMove);
			document.addEventListener('mouseout', onMouseOut);
		}
		isOpen = true;
	}

	function onMouseOver(e) {
		el = e.target;

		if (isTouch) {
			hasMoved = false;
			document.addEventListener('touchmove', onTouchMove);
			document.addEventListener('touchend', onTouchEnd);
		} else if (el.tagName === 'IMG') {
			openImage(el);
		}
	}

	function getMouse(mouseEvent) {
		if (!bound) return {};

		var mouseX, mouseY;
		if (isTouch) {
			mouseEvent.preventDefault();
			mouseX = mouseEvent.touches[0].pageX;
			mouseY = mouseEvent.touches[0].pageY;
		}
		else {
			mouseX = mouseEvent.clientX;
			mouseY = mouseEvent.clientY;
		}

		// Snap mouse to boundaries if they go out of bound upto some distance
		if (bound.left - mouseX > 0 && bound.left - mouseX < snapDistance) {
			mouseX = bound.left;
		} else if (mouseX - bound.right > 0 && mouseX - bound.right < snapDistance) {
			mouseX = bound.right;
		}

		if (bound.top - mouseY > 0 && bound.top - mouseY < snapDistance) {
			mouseY = bound.top;
		} else if (mouseY - bound.bottom > 0 && mouseY - bound.bottom < snapDistance) {
			mouseY = bound.bottom;
		}

		return { x: mouseX, y: mouseY };
	}

	function onMove(e) {
		if (!img || !bound) return;
		var mouse = getMouse(e), top;

		if (img.height > H) {
			top = bound.top - mouse.y;
			img.style.top = 'calc(' + (1 * top / bound.height * (img.height - H)) + 'px)';
		}

		if (isOutside && (mouse.x > bound.right || mouse.x < bound.left || mouse.y > bound.bottom || mouse.y < bound.top)) {
			reset();
		}
	}

	function reset() {
		img.style.opacity = underlay.style.opacity = 0;
		el = null;
		bound = null;
		isOutside = false;
		document.removeEventListener('mousemove', onMove);
		document.removeEventListener('mouseout', onMouseOut);

		if (isTouch) {
			img.style.transform = 'translate(-50%, -50%) scale(1)';
		}
		else {
			img.style.transform = 'translate(0%, 0%) scale(0.9)';
		}
	}

	function onMouseOut(e) {
		var mouse = getMouse(e);

		if (!(mouse.x > bound.right || mouse.x < bound.left || mouse.y > bound.bottom || mouse.y < bound.top)) {
			isOutside = true;
			return;
		}

		reset();
	}

	window.addEventListener('resize', function() {
		W = window.innerWidth;
		H = window.innerHeight;
	});

})();