export interface User{
    id: number
    userName: string
    email: string
    password: string
    personId: number
    isDeleted?: boolean
}