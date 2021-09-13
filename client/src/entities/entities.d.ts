interface AnimeDetail {
    key: string
    image: string
    title: string
    fileName: string
    views: number
    link: string
    iframe: boolean
    episode: int
}

interface Animes {
    key: string
    image: string
    fileName: string
    title: string
    titleVie: string
    description: string
    trainer: string
    movieDuration: string
    dateRelease: Date
    episode: number
    episodeMoment: number
    linkStart: string
    linkEnd: string
    isStatus: number
    banner: string
    fileNameBanner: string
    isBanner: boolean
    collectionId: string
    categoryKey: string
    dateCreated: Date
    views: number
    viewDays: number
    viewMonths: number
    viewWeeks: number
    series: string
    type: string
}

interface AnimeSeries {
    key: string
    ordinal: number
    animeTitle: string
    yearProduce: number
    session: string
    linkStart: string
    linkEnd: string
}

interface AnimeUser {
    displayName: string
    email: string
    isEmailVerified: boolean
    role: string
}

interface Categories {
    key: string
    title: string
    description: string
}

interface Collections {
    key: string
    image: string
    title: string
    fileName: string
    dateCreated: Date
}

interface Comment {
    key: string
    userLocal: string
    displayName: string
    photoUrl: string
    message: string
    when: Date
    replyComment: string
    likes: number
}

interface Notification {
    key: string
    userRevice: string
    message: string
    linkNotify : string
    when : Date
    isRead: boolean
    title: string
}
