rate();
document.querySelectorAll("#product").forEach(element => {
    element.onclick = () => {
        element.childNodes[1].click();
    }
});
