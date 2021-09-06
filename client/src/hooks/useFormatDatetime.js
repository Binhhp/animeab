
export default function formatDate(time){
    return (new Date(time)).toLocaleString('en-US', {
        weekday: "short",
        day: "numeric",
        year: "numeric",
        month: "long"
    })
};