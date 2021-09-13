
export default function Expand() {
    const expand = (): void => {
        var classElement = (document.getElementById("animeAB") as HTMLElement).classList;
        var iconExpand = (document.getElementById("icon-expand") as HTMLElement);

        var btnExpand = document.getElementById("expand") as HTMLElement;
        if(classElement.contains('expand')) {
            (document.getElementById("expand") as HTMLElement).innerText = "Mở rộng";
            iconExpand.classList.remove("fa-compress");
            iconExpand.classList.add("fa-expand");
            classElement.remove("expand");
        }
        else{
            btnExpand.innerText = "Thu nhỏ";
            iconExpand.classList.add("fa-compress");
            iconExpand.classList.remove("fa-expand");
            classElement.add("expand");
        }
    }
    
    return (
        <div onClick={expand} className="font-action mr-4">
            <i id="icon-expand" className="fas fa-expand mr-2"></i>
            <span id="expand">Mở rộng</span>
        </div>
    )
}