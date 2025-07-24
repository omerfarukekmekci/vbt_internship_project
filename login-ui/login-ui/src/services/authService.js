import axios from 'axios';

const BASE_API_URL = import.meta.env.VITE_API_BASE_URL;

export const register = async (username, email, password) => {
    try {
        const response = await fetch(`${BASE_API_URL}/register`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username, email, password }),
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || errorData.detail || "Registration failed.");
        }

        const data = await response.json();
        if (data.token) {
            localStorage.setItem('user', JSON.stringify(data));
        }
        return data;
    } catch (error) {
        console.error("Registration error:", error);
        throw error;
    }
};

export const login = async (email, password) => {
    try {
        // ASSUMPTION: Backend Login method now expects data in Request Body (not query parameters)
        const response = await fetch(`${BASE_API_URL}/login`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({ email, password }),
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || errorData.detail || "Invalid credentials.");
        }

        const data = await response.json();
        if (data.token) {
            localStorage.setItem('user', JSON.stringify(data));
        }
        return data;
    } catch (error) {
        console.error("Login error:", error);
        throw error;
    }
};

export const logout = () => {
    localStorage.removeItem('user');
};

export const getCurrentUser = () => {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
};

// Optional: Axios interceptor setup. Uncomment if you are using Axios and need to automatically attach tokens.
/*
export const setupAxiosInterceptors = () => {
    axios.interceptors.request.use(
        (config) => {
            const user = getCurrentUser();
            if (user && user.token) {
                config.headers.Authorization = 'Bearer ' + user.token;
            }
            return config;
        },
        (error) => {
            return Promise.reject(error);
        }
    );
};
*/