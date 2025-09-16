// wwwroot/js/Index/indexProjects.js
import { projects } from '/js/data/projectData.js';

(() => {
    const container = document.querySelector('.section-2-projects');
    if (!container || !Array.isArray(projects)) return;

    container.innerHTML = ''; // Clear any existing content

    projects.forEach(project => {
        // Defensive defaults
        const {
            title = 'Untitled Project',
            description = '',
            imagePath = '/images/placeholder.png',
            technologies = [],
            link = '#'
        } = project;

        const card = document.createElement('div');
        card.className = 'project-card';

        // Build tech tags safely
        const techTags = Array.isArray(technologies)
            ? technologies.map(tech => `<span class="tag">${escapeHtml(tech)}</span>`).join('')
            : '';

        // Fill card markup
        card.innerHTML = `
      <img src="${imagePath}" alt="${escapeHtml(title)} image" class="project-image" loading="lazy" />
      <h3 class="project-title">${escapeHtml(title)}</h3>
      <p class="project-desc">${escapeHtml(description)}</p>
      <div class="tech-tags">${techTags}</div>
      <a href="${link}" class="project-link" target="_blank" rel="noopener noreferrer">Learn More</a>
    `;

        container.appendChild(card);

        // Preload image
        const preload = new Image();
        preload.src = imagePath;
    });

    // Simple HTML escaper to prevent injection
    function escapeHtml(str) {
        return String(str)
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#039;');
    }
})();
