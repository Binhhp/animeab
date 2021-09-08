
export default function Expand() {
    const expand = () => {
        var classElement = document.getElementById("animeAB").classList;
        if(classElement.contains('expand')) {
            document.getElementById("expand").innerText = "Mở rộng";
            document.getElementById("icon-expand").classList.remove("fa-compress");
            document.getElementById("icon-expand").classList.add("fa-expand");
            classElement.remove("expand");
        }
        else{
            document.getElementById("expand").innerText = "Thu nhỏ";
            document.getElementById("icon-expand").classList.add("fa-compress");
            document.getElementById("icon-expand").classList.remove("fa-expand");
            classElement.add("expand");
        }
        return;
    }
    
    return (
        <div onClick={() => expand()} className="font-action mr-4">
            <i id="icon-expand" className="fas fa-expand mr-2"></i>
            <span id="expand">Mở rộng</span>
        </div>
    )
}