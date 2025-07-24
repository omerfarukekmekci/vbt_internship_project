import { Link } from 'react-router-dom';

const ForgotPasswordPage = () => {
  return (
    <div className="min-h-screen flex">
      {/* Solda resim */}
      <div 
        className="hidden md:flex flex-1 items-center justify-center bg-gray-200"
        style={{
          backgroundImage: `url('/bilgisayar.png')`,
          backgroundSize: 'contain',
          backgroundRepeat: 'no-repeat',
          backgroundPosition: 'center',
        }}
      >
        {/* Resim */}
      </div>

      {/* Sağda form*/}
      <div className="flex-1 flex items-center justify-center bg-white p-8 md:p-12">
        <div className="w-full max-w-lg">
          <h2 className="text-4xl font-bold text-center mb-6 text-gray-800">Forgot Your Password?</h2>
          <p className="text-center text-gray-600 mb-8 text-lg font-medium">
            No worries! Enter your email and we'll send you a reset link.
          </p>

          <form>
            <div className="mb-6">
              <label htmlFor="email" className="block text-gray-700 text-base font-semibold mb-2">Email Address</label>
              <input
                type="email"
                id="email"
                className="w-full px-5 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 text-lg font-medium"
                placeholder="Enter your email"
                required
              />
            </div>
            <button
              type="submit"
              className="w-full bg-blue-600 text-white py-4 rounded-lg hover:bg-blue-700 transition duration-300 font-bold text-lg"
            >
              Send Reset Link
            </button>
          </form>

          <p className="text-center text-gray-600 mt-8 text-base font-medium">
            Remember your password?{' '}
            <Link to="/" className="text-blue-600 hover:underline font-semibold">
              Login
            </Link>
          </p>
        </div>
      </div>
    </div>
  );
};

export default ForgotPasswordPage;