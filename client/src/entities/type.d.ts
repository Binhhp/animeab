
type StateAction = {
    loading?: boolean
    data?: any[] | any
    error?: string | any
}

interface IActionAnime {
    type: string
    payload?: Animes[] | string
}

interface IActionAnimeDetail {
    type: string
    payload?: AnimeDetail[] | AnimeDetail | string
}

interface IActionAnimeUser {
    type: string
    payload?: AnimeUser[] | string
}

interface IActionCategory {
    type: string
    payload?: Categories[] | string
}

interface IActionCollect {
    type: string
    payload?: Collections[] | string
}

interface IActionComment {
    type: string
    payload?: Comment[] | Comment | string
}

interface IActionNotify {
    type: string
    payload?: Notification[] | Notification | string
}

