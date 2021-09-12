
export interface IAnimeState {
    loading: boolean,
    data: [] | {},
    error: string
}

export type Action = {
    type: string,
    payload: any
}

export const initialState: IAnimeState = {
    loading: false,
    data: [],
    error: ""
};