CenterCardClick();
function search(){
    let search = document.getElementById('search').value.toLowerCase();
    let center = document.querySelectorAll('#center');
    for (let i = 0; i < center.length; i++) {
        let a = center[i].getElementsByTagName('h2')[0];
        let value = a.innerHTML||a.innerText||a.textContent;
        if (value.toLowerCase().indexOf(search) > -1) {
            center[i].style.display = '';
        }
        else
        {
            center[i].style.display = 'none';
        }
    }
}
/*window.onscroll = () => {
    if (document.documentElement.scrollTop + document.documentElement.clientHeight >= document.body.scrollHeight -10) {
        for (let i = 0; i < 6; i++){
            let e = document.getElementById("center").cloneNode(true);
            console.log(e);
            document.getElementById("all-centers").appendChild(e);
            CenterCardClick();
        }
        height = document.documentElement.scrollHeight - document.documentElement.clientHeight;
        window.addEventListener("scroll", () => {
            scrollTop = document.documentElement.scrollTop;
            scroller.style.width = (scrollTop / height) * 100 + "%";
        })
    }
}*/