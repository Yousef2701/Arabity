document.getElementById("add photo").onclick = ()=>{
    document.querySelector("#file").click();
    document.querySelector("#file").addEventListener("change", function () {
        document.getElementById("img").src = URL.createObjectURL(document.querySelector("#file").files[0]);
        document.getElementById("add photo").classList.add("d-none");
    document.getElementById("img").classList.remove("d-none");
    });
}
document.getElementById("img").onclick = ()=>{
    document.querySelector("#file").click();
}
document.getElementById("update-center-info").onclick = ()=>{
    document.getElementById("update-center-info").children[0].click();
}
document.querySelectorAll("#edit").forEach(element => {
    element.onclick = () => {
        element.children[0].click();;
    }
});
document.querySelectorAll("#remove").forEach(element => {
    element.onclick = () => {
        element.parentElement.remove();
    }
});
