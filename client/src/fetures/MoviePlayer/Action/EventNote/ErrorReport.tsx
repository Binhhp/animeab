import { toast } from "react-toastify";

export default function ErrorReport() {
    const error = (): void => {
        toast.success("Cảm ơn bạn đã báo lỗi! Chúng tôi sẽ khắc phục sự cố trong tích tắc!", {
            autoClose: 5000
        });
    };
    return (
        <div className="font-action" onClick={error}>
            <i className="fas fa-exclamation mr-2"></i><span>Báo lỗi</span>
        </div>
    )
}