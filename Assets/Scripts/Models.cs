using UnityEngine;

[System.Serializable]
public class Models
{
    [System.Serializable]
    public class File
    {
        public string type;
        public string url;

        [System.Serializable]
        public class Array
        {
            public Models.File[] elements;
        }
    }

    [System.Serializable]
    public class Content
    {
        public int order;
        public bool enabled;
        public Models.File file;
        public string desc;

        [System.Serializable]
        public class Array
        {
            public Models.Content[] elements;
        }
    }

    [System.Serializable]
    public class Gallery
    {
        public string name;
        public int limit;
        public Models.Content[] contents;

        [System.Serializable]
        public class Array
        {
            public Models.Gallery[] elements;
        }
    }

    [System.Serializable]
    public class Target
    {
        public string name;
        public Models.Gallery gallery;

        [System.Serializable]
        public class Array
        {
            public Models.Target[] elements;
        }
    }
}
