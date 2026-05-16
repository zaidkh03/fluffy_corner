let current = 0;

function goSlide(n) {
    const slides = document.querySelectorAll('.hero-slide');
    const dots = document.querySelectorAll('.dot');

    if (!slides.length || !dots.length) return;

    slides[current].classList.remove('active');
    dots[current].classList.remove('active');

    current = n;

    slides[current].classList.add('active');
    dots[current].classList.add('active');
}

function changeSlide(dir) {
    const slides = document.querySelectorAll('.hero-slide');

    if (!slides.length) return;

    const next = (current + dir + slides.length) % slides.length;
    goSlide(next);
}

setInterval(function () {
    changeSlide(1);
}, 4000);