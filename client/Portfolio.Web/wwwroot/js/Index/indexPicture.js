(() => {
    const imageSection = document.querySelector('.split-section.image');
    const heroImage = document.getElementById('heroFace');
    if (!imageSection || !heroImage) return;

    const SRC_DEFAULT = '/images/PicDarkOrange.jpg';
    const SRC_HOVER = '/images/LightOrange.jpg';

    // Preload images to avoid hover flicker
    [SRC_DEFAULT, SRC_HOVER].forEach(src => { const i = new Image(); i.src = src; });

    // Make the area obviously interactive + keyboard accessible
    imageSection.style.cursor = 'pointer';
    imageSection.setAttribute('role', 'button');
    imageSection.tabIndex = 0;

    // Hover swap (only on devices that actually support hover)
    if (window.matchMedia('(hover: hover)').matches) {
        imageSection.addEventListener('mouseenter', () => { heroImage.src = SRC_HOVER; }, { passive: true });
        imageSection.addEventListener('mouseleave', () => { heroImage.src = SRC_DEFAULT; }, { passive: true });
    }

    // Click / keyboard -> scroll to About; honor reduced motion
    const prefersReduced = window.matchMedia('(prefers-reduced-motion: reduce)').matches;

    function scrollToAbout() {
        const aboutSection = document.getElementById('aboutMeSection');
        if (!aboutSection) return;
        aboutSection.scrollIntoView({ behavior: prefersReduced ? 'auto' : 'smooth', block: 'start' });
    }

    imageSection.addEventListener('click', scrollToAbout);
    imageSection.addEventListener('keydown', (e) => {
        if (e.key === 'Enter' || e.key === ' ') {
            e.preventDefault();
            scrollToAbout();
        }
    });
})();
