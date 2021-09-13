
export default function Light() {
    const turnLight = (): void => {
        var classElement = (document.getElementById("animeAB") as HTMLElement).classList;
        var ligth = document.getElementById("light") as HTMLElement;

        if(classElement.contains('light-turn')) {
            ligth.innerText = "Tắt đèn";
            classElement.remove("light-turn");
        }
        else{
            ligth.innerText = "Bật đèn";
            classElement.add("light-turn");
        }
    } 

    return (
        <div onClick={turnLight} className="font-action mr-4">  
            <i className="fas fa-lightbulb mr-2"></i>
            <span id="light">Tắt đèn</span>
            <span className="on_off ml-2">On</span>
        </div>
    )
}