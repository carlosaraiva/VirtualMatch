export interface User {
    username: String;
    token: String;
}

export function newUser() : User {
    return {
        username: '',
        token: ''
    };
}