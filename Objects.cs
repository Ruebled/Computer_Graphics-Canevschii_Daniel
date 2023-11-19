using System;
using System.Collections.Generic;

namespace Graphics_Homework
{
    class Objects
    {
        private int SelectedObject;
        private List<Object> objects;

        public Objects() 
        {
            objects = new List<Object>();
            SelectedObject = 0;
        }

        public Object Selected()
        {
            if (objects[SelectedObject] == null)
            {
                return new Object();
            }
            return objects[SelectedObject];
        }

        public void Add(Object obj)
        {
            if(obj == null)
            {
                Logging.print("Added an null object to objects list");
                return;
            }
            objects.Add(obj);
        }

        public void Add(String FileName)
        {
            if (FileName.Trim().Length > 0)
            {
                objects.Add(new Object(FileName));
                Logging.print("Added object of " + FileName + " filename.");
            }

           
        }

        public void UpdatePosition()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].UpdatePosition();
            }
        }

        public void Draw()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Draw();
            }
        }
    }
}
