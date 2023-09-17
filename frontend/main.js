window.addEventListener('DOMConectLoads', (event) =>{
    getVisitCount();
})

const funcationAPi = '';

const getVisitCount = () => {
    let count =30;
    fetch(functionApi).then(response => {
        return Response.json()
    }).then(response =>{
        console.log("Website called functionAPI");
        count = response.count;
        document.getElementById("counter").innertext = count;
    }).catch(function(error){
        console.log(error);
    });
    return count;
}