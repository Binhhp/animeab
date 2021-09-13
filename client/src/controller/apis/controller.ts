
const baseUrl = "api";
export class ApiController {
    
    static GET_ANIME(
        animeKey = "", 
        stus = 0, 
        banner = false,
        sort = "date",
        take = 0,
        trend = 0): string {
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
    }

    static GET_ANIME_FILTER(keyword = ""): string {
        let filterApi = ApiController.GET_ANIME();
        if(keyword !== ""){
            filterApi += `&q=${keyword}`;
        }
        return filterApi;
    }

    static GET_EPISODE(animeKey: string, episode = ""): string {
        let episodeApis = `${baseUrl}/animes/episodes?id=${animeKey}`;
        if(episode !== "")
        {
            episodeApis += `&episode=${episode}`
        }
        return episodeApis;
    }

    static GET_ANIMECATE_OF_ANIMECOLLECT(categoryKey = "", collectionId = ""): string {
        let cateOfCollectApi = `${baseUrl}/animes?des=views&des=date&`;
        if(categoryKey !== ""){
            cateOfCollectApi += `cate=${categoryKey}`;
        }
        if(collectionId !== ""){
            cateOfCollectApi += `collect=${collectionId}`;
        }
        return cateOfCollectApi;
    }

    static GET_ANIME_RANK(sort: string):string {
        let url = `${baseUrl}/animes?des=${sort}&take=10&completed=3`;
        return url;
    }

    static GET_ANIME_NEW(take = 0):string {
        let url = `${baseUrl}/animes?stus=2&des=date&des=views`;
        if(take > 0) {
            url += `&take=${take}`;
        }
        return url;
    }

    static GET_ANIME_OFFER(categoryKey: string, animeKey: string): string {
        let url = `api/animes?offer=true&cate=${categoryKey}&id=${animeKey}&random=true&take=17`;
        return url;
    }

    static GET_ANIME_RELATE(categoryKey: string, animeKey: string, sort: string): string {
        var url = `/api/animes?cate=${categoryKey}&id=${animeKey}&des=${sort}&take=10&completed=3`;
        return url;
    }

    static UPDATE_VIEW(animeKey: string, animeDetailKey = ""): string {
        let viewApi = `${baseUrl}/animes/${animeKey}${animeDetailKey !== "" ? `/${animeDetailKey}/` : '/'}views`;
        return viewApi;
    }

    static GET_CATES(): string {
        return `${baseUrl}/categories`
    }
    
    static GET_COLLECTES(): string {
        return `${baseUrl}/collections`
    }

    static GET_COMMENTS(animeKey: string, sort = 'lastest'): string {
        return `${baseUrl}/comments?id=${animeKey}&sort=${sort}`;
    }

    static SEND_COMMENT(animeKey: string, link_notify: string = "", receiver: string = ""): string {
        let commentApi = `/api/comments?id=${animeKey}`;
    
        if(link_notify !== "" && receiver !== "")
            commentApi += `&receiver=${receiver}&link_notify=${link_notify}`;

        return commentApi;
    }
    
    static REPLY_COMMENT(animeKey: string, user_reply: string): string {
        let replyCommentApi = ApiController.GET_COMMENTS(animeKey);
        replyCommentApi += `&user_reply=${user_reply}&sort="lastest"`;
        return replyCommentApi;
    }

    static NOTIFY(user_uid: string, notify = "", isCount = false): string {
        let notifyApi = `${baseUrl}/notification?user=${user_uid}`;
        if(notify !== "")
            notifyApi += `&notify=${notify}`;

        if(isCount) {
            notifyApi += `&count=true`;
        }

        return notifyApi;
    }

    static SERIES(key: string): string {
        let url = `${baseUrl}/series/${key}`;
        return url;
    }

    static LIKE_COMMENT(id: string, idComment: string): string {
        return `${baseUrl}/likes?id=${id}&idComment=${idComment}`;
    }
}