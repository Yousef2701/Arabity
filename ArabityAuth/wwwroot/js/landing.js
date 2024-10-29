// Get all the accordion buttons
var acc = document.getElementsByClassName("accordion");

// Loop through the accordion buttons and add a click event listener to each
for (var i = 0; i < acc.length; i++) {
  acc[i].addEventListener("click", function() {
    // Toggle the active class on the button to change its background color
    this.classList.toggle("active");

    // Get the panel that is next to the button
    var panel = this.nextElementSibling;

    // If the panel is currently hidden, show it. Otherwise, hide it.
    if (panel.style.display === "block") {
      panel.style.display = "none";
    } else {
      panel.style.display = "block";
    }
  });
}
