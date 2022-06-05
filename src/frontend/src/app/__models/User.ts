export interface User {
    username: String;
    jwtToken: String;
}

export function newUser() : User {
    return {
        username: '',
        jwtToken: ''
    };
}