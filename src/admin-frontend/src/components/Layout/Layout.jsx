// Layout.js
export default function Layout({ children }) {
    return (
      <div className="flex h-screen">
        <Sidebar />
        <div className="flex-1 ml-64 p-6 overflow-auto bg-gray-100">
          {children}
        </div>
      </div>
    );
  }