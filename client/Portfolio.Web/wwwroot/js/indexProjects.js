import { projects } from '/js/data/projectData.js';

const container = document.querySelector('.section-2-projects');
container.innerHTML = ''; // Clear if anything is there

projects.forEach(project => {
    const card = document.createElement('div');
    card.className = 'project-card';

    card.innerHTML = `
    <img src="${project.imagePath}" alt="${project.title} image" class="project-image" />
    <h3 class="project-title">${project.title}</h3>
    <p class="project-desc">${project.description}</p>
    <div class="tech-tags">
      ${project.technologies.map(tech => `<span class="tag">${tech}</span>`).join('')}
    </div>
    <a href="${project.link}" class="project-link">Learn More</a>
  `;

    container.appendChild(card);
});
