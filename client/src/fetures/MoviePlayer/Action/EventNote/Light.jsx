
export default function Light() {
    const turnLight = () => {
        var classElement = document.getElementById("animeAB").classList;
        if(classElement.contains('light-turn')) {
            document.getElementById("light").innerText = "Tắt đèn";
            classElement.remove("light-turn");
        }
        else{
            document.getElementById("light").innerText = "Bật đèn";
            classElement.add("light-turn");
        }
        return;
    } 

    return (
        <div onClick={() => turnLight()} className="font-action mr-4">  
            <i className="fas fa-lightbulb mr-2"></i>
            <span id="light">Tắt đèn</span>
            <span className="on_off ml-2">On</span>
        </div>
    )
}