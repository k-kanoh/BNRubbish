namespace BerorinApp
{
    public static class UtilFileExtension
    {
        public static string Combine(this DirectoryInfo dinfo, params string[] name)
        {
            return Path.Combine(new[] { dinfo.FullName }.Concat(name.Select(x => x ?? "")).ToArray());
        }

        public static DirectoryInfo SubDirectory(this DirectoryInfo dinfo, params string[] name)
        {
            return new DirectoryInfo(Combine(dinfo, name));
        }

        /// <remarks>
        /// FileInfoを使ってExists等の判定をしないで下さい。(キャッシュを見に行くため)
        /// Exists等の判定をする場合は必ず直前にRefresh()して下さい。
        /// </remarks>
        public static FileInfo File(this DirectoryInfo dinfo, params string[] name)
        {
            return new FileInfo(Combine(dinfo, name));
        }

        public static string GetFileNameWithoutExtension(this FileInfo finfo)
        {
            return Path.GetFileNameWithoutExtension(finfo.FullName);
        }

        public static string GetNoChangeHash(this FileInfo finfo)
        {
            return $"{finfo.FullName}{finfo.Length}{finfo.LastWriteTime.Ticks}".GetSha1Hash();
        }

        public static FileStream OpenReadNoLock(this FileInfo finfo)
        {
            return new FileStream(finfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
    }
}
