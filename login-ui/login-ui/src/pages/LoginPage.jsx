import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { FaEye, FaEyeSlash } from 'react-icons/fa';
import { login } from '../services/authService';
import { useAuth } from '../context/AuthContext';

const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [passwordVisible, setPasswordVisible] = useState(false);
  const [errors, setErrors] = useState({});
  const { loginUser } = useAuth();
  const navigate = useNavigate();

  const togglePasswordVisibility = () => {
    setPasswordVisible(!passwordVisible);
  };

  const validateForm = () => {
    const newErrors = {};
    if (!email) {
      newErrors.email = 'Email address is required.';
    } else if (!/\S+@\S+\.\S+/.test(email)) {
      newErrors.email = 'Email address is invalid.';
    }
    if (!password) {
      newErrors.password = 'Password is required.';
    } else if (password.length < 6) {
      newErrors.password = 'Password must be at least 6 characters.';
    }
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (validateForm()) {
      try {
        const data = await login(email, password);
        loginUser(data.user, data.token);
        navigate("/dashboard");
      } catch (error) {
        alert(error.message);
      }
    }
  };

  return (
    <div className="min-h-screen flex">
      <div 
        className="hidden md:flex flex-1 items-center justify-center bg-gray-200"
        style={{
          backgroundImage: `url('/bilgisayar.png')`,
          backgroundSize: 'contain',
          backgroundRepeat: 'no-repeat',
          backgroundPosition: 'center',
        }}
      />
      <div className="flex-1 flex items-center justify-center bg-white p-8 md:p-12">
        <div className="w-full max-w-lg">
          <h2 className="text-4xl font-bold text-center mb-6 text-gray-800">Welcome Back!</h2>
          <p className="text-center text-gray-600 mb-8 text-lg font-medium">Sign in to continue to your account.</p>
          <form onSubmit={handleSubmit} noValidate>
            <div className="mb-6">
              <label htmlFor="email" className="block text-gray-700 text-base font-semibold mb-2">Email Address</label>
              <input
                type="email"
                id="email"
                className={`w-full px-5 py-3 border rounded-lg focus:outline-none focus:ring-2 text-lg font-medium ${errors.email ? 'border-red-500 focus:ring-red-500' : 'border-gray-300 focus:ring-blue-500'}`}
                placeholder="Enter your email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
              {errors.email && <p className="text-red-500 text-sm mt-1">{errors.email}</p>}
            </div>
            <div className="mb-4 relative">
              <label htmlFor="password" className="block text-gray-700 text-base font-semibold mb-2">Password</label>
              <input
                type={passwordVisible ? 'text' : 'password'}
                id="password"
                className={`w-full px-5 py-3 border rounded-lg focus:outline-none focus:ring-2 text-lg pr-12 font-medium ${errors.password ? 'border-red-500 focus:ring-red-500' : 'border-gray-300 focus:ring-blue-500'}`}
                placeholder="Enter your password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
              <span
                className="absolute inset-y-0 right-0 pr-3 flex items-center cursor-pointer mt-8"
                onClick={togglePasswordVisibility}
              >
                {passwordVisible ? (
                  <FaEyeSlash className="h-6 w-6 text-gray-500 hover:text-gray-700" />
                ) : (
                  <FaEye className="h-6 w-6 text-gray-500 hover:text-gray-700" />
                )}
              </span>
              {errors.password && <p className="text-red-500 text-sm mt-1">{errors.password}</p>}
            </div>
            <div className="flex items-center justify-between mb-8">
              <div className="flex items-center">
                <input
                  id="remember-me"
                  name="remember-me"
                  type="checkbox"
                  className="h-4 w-4 text-blue-600 focus:ring-blue-500 border-gray-300 rounded"
                />
                <label htmlFor="remember-me" className="ml-2 block text-sm text-gray-900 font-medium">
                  Remember me
                </label>
              </div>
              <Link to="/forgot-password" className="text-sm text-blue-600 hover:underline font-medium">
                Forgot password?
              </Link>
            </div>
            <button
              type="submit"
              className="w-full bg-blue-600 text-white py-4 rounded-lg hover:bg-blue-700 transition duration-300 font-bold text-lg"
            >
              Login
            </button>
          </form>
          <p className="text-center text-gray-600 mt-8 text-base font-medium">
            Don't have an account?{' '}
            <Link to="/register" className="text-blue-600 hover:underline font-semibold">
              Register
            </Link>
          </p>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
