import React, { useState } from 'react';
import styles from './ForgotPassword.module.css';
import api from "../../services/api";

const ForgotPassword = () => {
    const [email, setEmail] = useState('');
    const [error, setError] = useState(null);
    const [successMessage, setSuccessMessage] = useState(null);

    const handleSubmit = async (event) => {
        event.preventDefault(); // предотвращаем перезагрузку страницы
        if (!email) {
            setError('Please enter your email.');
        } else {
            setError(null);
            try
            {
                const resp = await api.post(`/api/Auth/forgot-password`, {
                    email: email
                });
                if(resp.status === 200)
                {
                    setSuccessMessage('Password reset link has been sent to your email.');
                }
            }
            catch (e)
            {
                console.log(e)
            }
        }
    };

    return (
        <div className={styles.forgotPasswordContainer}>
            <div className={styles.forgotPasswordCard}>
                <h2 className={styles.forgotPasswordTitle}>Forgot Password</h2>
                <form onSubmit={handleSubmit} className={styles.forgotPasswordForm}>
                    {error && <div className="text-danger mb-3">{error}</div>}
                    {successMessage && <div className="text-success mb-3">{successMessage}</div>}
                    <div className={styles.forgotPasswordField}>
                        <label htmlFor='email' className={styles.forgotPasswordLabel}>
                            Email
                        </label>
                        <input
                            id='email'
                            type='email'
                            className={styles.forgotPasswordInput}
                            placeholder='Enter your email'
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </div>
                    <button type='submit' className={styles.forgotPasswordButton}>
                        Submit
                    </button>
                </form>
            </div>
        </div>
    );
}

export default ForgotPassword;
