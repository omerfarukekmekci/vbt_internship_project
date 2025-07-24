const DashboardPage = () => {
  return (
    <div className="min-h-screen flex items-center justify-center bg-green-100">
      <div className="bg-white p-8 rounded-lg shadow-lg text-center">
        <h2 className="text-4xl font-bold text-green-700 mb-4">Welcome to Your Dashboard!</h2>
        <p className="text-lg text-gray-600">You have successfully logged in.</p>
        <p className="mt-8 text-gray-500">
          (This is a protected page. Once the backend is ready, you'll only see this page if you're authenticated.)
        </p>
      </div>
    </div>
  );
};

export default DashboardPage;