export interface Login {
    username: String;
    password: String;
}

export function newLogin() : Login {
    return {
        username: '',
        password: ''
    };
}