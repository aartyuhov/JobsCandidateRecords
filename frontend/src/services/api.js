import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7087',
    withCredentials: true
});

api.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('auth_token');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error)
);

api.interceptors.response.use(
    (response) => response,
    async (error) => {
        const originalRequest = error.config;
        console.log(error)
        if (error.response.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;
            try {
                const refreshToken = localStorage.getItem('refresh_token');
                const response = await axios.post('/api/Auth/refreshtoken', { refreshToken });
                const { token } = response.data;

                localStorage.setItem('auth_token', token);

                originalRequest.headers.Authorization = `Bearer ${token}`;
                return axios(originalRequest);
            } catch (error) {
                localStorage.removeItem('username');
                localStorage.removeItem('auth_token');
                localStorage.removeItem('refresh_token');
                window.location.replace(window.location.origin);
            }
        }

        return Promise.reject(error);
    }
);


export default api;
