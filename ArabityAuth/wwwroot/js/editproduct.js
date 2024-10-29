document.getElementById("add photo").onclick = ()=>{
    document.querySelector("#file").click();
    document.querySelector("#file").addEventListener("change", function () {
        document.getElementById("img").src = URL.createObjectURL(document.querySelector("#file").files[0]);
        document.getElementById("add photo").classList.add("d-none");
    document.getElementById("img").classList.remove("d-none");
    });
}