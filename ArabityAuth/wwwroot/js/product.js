ProductCardClick();
rate();
let count = document.getElementById("count").value;

document.getElementById("increment").onclick = () => {
    count++;
    document.getElementById("count").value = count;
}
document.getElementById("decrement").onclick = () => {
    if (count != 1) {
        count--;
    }
    document.getElementById("count").value = count;
}
document.getElementById("count").addEventListener("change", () => {
    if (document.getElementById("count").value < 1) {
        document.getElementById("count").value = 1;
    }
    count = document.getElementById("count").value;
})