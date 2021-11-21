document.getElementById("about-link").addEventListener("click", (e) => {
  e.preventDefault();
  document.getElementById("about").scrollIntoView({
    behavior: "smooth",
  });
});

document.getElementById("images-link").addEventListener("click", (e) => {
  e.preventDefault();
  document.getElementById("images").scrollIntoView({
    behavior: "smooth",
  });
});

document.getElementById("objectives-link").addEventListener("click", (e) => {
  e.preventDefault();
  document.getElementById("objectives").scrollIntoView({
    behavior: "smooth",
  });
});

document.getElementById("controls-link").addEventListener("click", (e) => {
  e.preventDefault();
  document.getElementById("controls").scrollIntoView({
    behavior: "smooth",
  });
});

document.getElementById("contact-link").addEventListener("click", (e) => {
  e.preventDefault();
  document.getElementById("contact").scrollIntoView({
    behavior: "smooth",
  });
});

document.getElementById("credit-link").addEventListener("click", (e) => {
  e.preventDefault();
  document.getElementById("credit").scrollIntoView({
    behavior: "smooth",
  });
});
