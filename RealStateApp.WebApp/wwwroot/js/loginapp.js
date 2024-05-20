const inputs = document.querySelectorAll(".input-field");
const toggle_btn = document.querySelectorAll(".toggle");
const main = document.querySelector("main");
const bullets = document.querySelectorAll(".bullets span");
const images = document.querySelectorAll(".image");
const formsWrap = document.querySelector('.forms-wrap');

inputs.forEach((inp) => {
  inp.addEventListener("focus", () => {
    inp.classList.add("active");
  });
  inp.addEventListener("blur", () => {
    if (inp.value != "") return;
    inp.classList.remove("active");
  });
});

toggle_btn.forEach((btn) => {
  btn.addEventListener("click", () => {
    main.classList.toggle("sign-up-mode");
    toggleScrollbarVisibility(); // Llamada a la función para ajustar el desplazamiento al cambiar el modo
  });
});

function moveSlider() {
  let index = this.dataset.value;

  let currentImage = document.querySelector(`.img-${index}`);
  images.forEach((img) => img.classList.remove("show"));
  currentImage.classList.add("show");

  const textSlider = document.querySelector(".text-group");
  textSlider.style.transform = `translateY(${-(index - 1) * 2.2}rem)`;

  bullets.forEach((bull) => bull.classList.remove("active"));
  this.classList.add("active");
}

bullets.forEach((bullet) => {
  bullet.addEventListener("click", moveSlider);
});

function toggleScrollbarVisibility() {
  if (main.classList.contains('sign-up-mode')) {
    formsWrap.style.overflowY = 'auto'; // Muestra la barra de desplazamiento en el modo de registro
  } else {
    formsWrap.style.overflowY = 'hidden'; // Oculta la barra de desplazamiento en el modo de inicio de sesión
  }
}

// Llama a la función inicialmente para establecer el estado inicial
toggleScrollbarVisibility();
