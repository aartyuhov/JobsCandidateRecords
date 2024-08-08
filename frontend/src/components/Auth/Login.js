import React, {useState} from 'react'
import styles from './Login.module.css'
import api from "../../services/api";

const Login = () => {
	const [error, setError] = useState(null);

	const handleSubmit = async event => {
		event.preventDefault()
		const email = event.target.email.value;
		const password = event.target.password.value;
		try {
			const result = await api.post('/api/Auth/login',
				{
					"email": email,
					"password": password,
					"rememberMe": true
				});
			if (result.status === 200) {
				localStorage.setItem('auth_token', result.data.token);
				localStorage.setItem('refresh_token', result.data.refreshToken);
				const user = await api.get('/api/Users/logged-with-role');
				if(user.status === 200)
				{
					localStorage.setItem('username', user.data.username);
					window.location.replace(window.location.origin);
				}
			}
		} catch (e) {
			if(e.response && e.response.status === 401)
			{
				setError("Invalid email or password. Please check and retry!");
			}
			else
			{
				console.log(e.message);
			}
		}
	}

	return (
		<div className={styles.loginContainer}>
			<div className={styles.loginCard}>
				<h2 className={styles.loginTitle}>HR Base Login</h2>
				<form onSubmit={handleSubmit} className={styles.loginForm}>
					<div className={styles.loginField}>
						{
							error ? (
								<div className="mb-3">
									<h5 className="text-danger">{error}</h5>
								</div>
							) : ""
						}
						<label htmlFor='email' className={styles.loginLabel}>
							Email
						</label>
						<input
							id='email'
							type='text'
							className={styles.loginInput}
							placeholder='Enter your email'
						/>
					</div>
					<div className={styles.loginField}>
						<label htmlFor='password' className={styles.loginLabel}>
							Password
						</label>
						<input
							id='password'
							type='password'
							className={styles.loginInput}
							placeholder='Enter your password'
						/>
					</div>
					<button type='submit' className={styles.loginButton}>
						Login
					</button>
				</form>
			</div>
		</div>
	)
}

export default Login