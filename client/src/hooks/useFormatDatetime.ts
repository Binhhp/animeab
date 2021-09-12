
export default function formatDate(time: Date | string | number) {
    return (new Date(time)).toLocaleString('en-US', {
        weekday: "short",
        day: "numeric",
        year: "numeric",
        month: "long"
    })
};