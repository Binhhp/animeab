
const baseUrl = "api";

export const controller = {
    GET_ANIME: (animeKey = "", stus = 0, banner = false, sort = "date", take = 0, trend = 0) => {
        let url = `${baseUrl}/animes`;

        if(animeKey !== "") url += `/${animeKey}`;
        else url += `?des=${sort}`;

        if(stus > 0) {
            url += `&completed=${stus}&take=12`;
        }
        
        if(trend > 0) {
            url += `&trend=${trend}`
        }

        if(take > 0) {
            url += `&take=${take}`;
        }

        if(banner) {
            url += `&banner=true`
        }

        return url;
    },

    GET_ANIME_FILTER: (keyword = "") => {
        let filterApi = controller.GET_ANIME();
        if(keyword !== ""){
            filterApi += `&q=${keyword}`;
        }
        return filterApi;
    },

    GET_EPISODE: (animeKey, episode = "") => {
        let episodeApis = `${baseUrl}/animes/episodes?id=${animeKey}`;
        if(episode !== "")
        {
            episodeApis += `&episode=${episode}`
        }
        return episodeApis;
    },

    GET_ANIMECATE_OF_ANIMECOLLECT: (categoryKey = "", collectionId = "") => {
        let cateOfCollectApi = `${baseUrl}/animes?des=views&des=date&`;
        if(categoryKey !== ""){
            cateOfCollectApi += `cate=${categoryKey}`;
        }
        if(collectionId !== ""){
            cateOfCollectApi += `collect=${collectionId}`;
        }
        return cateOfCollectApi;
    },

    GET_ANIME_RANK: (sort) => {
        let url = `${baseUrl}/animes?des=${sort}&take=10&completed=3`;
        return url;
    },

    GET_ANIME_NEW: (take = 0) => {
        let url = `${baseUrl}/animes?stus=2&des=date&des=views`;
        if(take > 0) {
            url += `&take=${take}`;
        }
        return url;
    },

    GET_ANIME_OFFER: (categoryKey, animeKey) => {
        let url = `api/animes?offer=true&cate=${categoryKey}&id=${animeKey}&random=true&take=17`;
        return url;
    },

    GET_ANIME_RELATE: (categoryKey, animeKey, sort) => {
        var url = `/api/animes?cate=${categoryKey}&id=${animeKey}&des=${sort}&take=10&completed=3`;
        return url;
    },

    UPDATE_VIEW: (animeKey, animeDetailKey = "") => {
        let viewApi = `${baseUrl}/animes/${animeKey}${animeDetailKey !== "" ? `/${animeDetailKey}/` : '/'}views`;
        return viewApi;
    },

    GET_CATES: `${baseUrl}/categories`,
    GET_COLLECTES: `${baseUrl}/collections`,

    GET_COMMENTS: (animeKey, sort = 'lastest') => {
        return `${baseUrl}/comments?id=${animeKey}&sort=${sort}`;
    },

    SEND_COMMENT: (animeKey, link_notify = "", receiver = "") => {
        let commentApi = `/api/comments?id=${animeKey}`;
    
        if(link_notify !== "" & receiver !== "")
            commentApi += `&receiver=${receiver}&link_notify=${link_notify}`;

        return commentApi;
    },
    
    REPLY_COMMENT: (animeKey, user_reply) => {
        let replyCommentApi = controller.GET_COMMENTS(animeKey);
        replyCommentApi += `&user_reply=${user_reply}&sort="lastest"`;
        return replyCommentApi;
    },

    NOTIFY: (user_uid, notify = "", isCount = false) => {
        let notifyApi = `${baseUrl}/notification?user=${user_uid}`;
        if(notify !== "")
            notifyApi += `&notify=${notify}`;

        if(isCount) {
            notifyApi += `&count=true`;
        }

        return notifyApi;
    },

    SERIES: (key) => {
        let url = `${baseUrl}/series/${key}`;
        return url;
    }

}