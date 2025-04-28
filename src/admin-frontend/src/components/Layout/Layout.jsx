import Sidebar from "./Sidebar";

export default function Layout({ children }) {
  return (
    <div className="flex h-screen">
      <Sidebar />
      <div className="flex-1 p-6 overflow-auto">{children}</div>
    </div>
  );
}