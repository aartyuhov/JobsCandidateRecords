import React from 'react'
import axios from 'axios'
import { useCookies } from 'react-cookie'
import styles from './Login.module.css'

const Login = () => {
	const [cookies, setCookie] = useCookies(['user'])
	if (cookies.user) window.location.replace(window.location.origin)

	const handleSubmit = async event => {
		event.preventDefault()
		const username = event.target.username.value
		const password = event.target.password.value
		try {
			const result = await axios.get('/api/Employees/1')
			if (result.status === 200) {
				setCookie('user', JSON.stringify(result.data))
				window.location.replace(window.location.origin)
			}
		} catch (e) {
			console.error(e.message)
			setCookie(
				'user',
				JSON.stringify({
					lastName: 'test',
					firstName: 'user',
				})
			)
			window.location.replace(window.location.origin)
		}
	}

	return (
		<div className={styles.loginContainer}>
			<div className={styles.loginCard}>
				<h2 className={styles.loginTitle}>HR Base Login</h2>
				<form onSubmit={handleSubmit} className={styles.loginForm}>
					<div className={styles.loginField}>
						<label htmlFor='username' className={styles.loginLabel}>
							Username
						</label>
						<input
							id='username'
							type='text'
							className={styles.loginInput}
							placeholder='Enter your username'
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