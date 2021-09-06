
const playList = (episodes, titleAnime, episodeItem) => {

    let listVideo = [];
    if(episodes && titleAnime && episodeItem){
        const title = episodeItem.title?.includes("Tập") 
                ? episodeItem.title 
                : `Tập ${episodeItem.episode} - ${episodeItem.title}`;

        listVideo.push({
            title: title,
            description: titleAnime,
            file: episodeItem?.link,
            image: episodeItem?.image,
            key: episodeItem.key,
            tracks: [{
                file:  episodeItem?.link,
                label: 'Vietnamese',
                kind: 'captions',
                'default': true
            }],
        });

        episodes?.filter(x => x.episode > episodeItem.episode).map((item, i) => (
            listVideo.push({
                title: item.title.includes("Tập") ? item.title : `Tập ${item.episode} - ${item.title}`,
                description: titleAnime,
                file: item.link,
                image: item.image,
                key: item.key,
                tracks: [{
                    file:  episodeItem.link,
                    label: 'Vietnamese',
                    kind: 'captions'
                }],
            })
        ))
    }
    
    return listVideo;
};

const hanlderError = { 
    intl: {
    en: {
        errors: {
        badConnection:
            "Không thể phát video này do sự cố với kết nối Internet của bạn.",
        cantLoadPlayer: "Xin lỗi, trình phát video không tải được.",
        cantPlayInBrowser:
            "Không thể phát video trong trình duyệt này.",
        cantPlayVideo: "Xin lỗi, trình phát video không tải được.",
        errorCode: "Code",
        liveStreamDown:
            "Luồng trực tiếp bị gián đoạn hoặc đã kết thúc.",
        protectedContent:
            "Đã xảy ra sự cố khi cung cấp quyền truy cập vào nội dung được bảo vệ.",
        technicalError:
            "Không thể phát video này do lỗi kỹ thuật."
        }
    }
    }
};


export { playList, hanlderError };