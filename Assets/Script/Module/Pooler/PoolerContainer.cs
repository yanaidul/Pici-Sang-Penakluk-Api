using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolerContainer : MonoBehaviour
{
    public PoolerObject[] prefabs;
    public Transform parent;

    public int poolSize = 5;
    public bool sequence = false;
    public bool reparent = false;

    public List<PoolerObject> allCollection = new List<PoolerObject>();
    public List<PoolerObject> collection = new List<PoolerObject>();
    public List<PoolerObject> availableCollection = new List<PoolerObject>();

    // Start is called before the first frame update
    void Awake()
    {
        if (!parent)
            parent = transform;

        FillPool();
    }

    void Start()
    {

    }

    protected virtual void FillPool()
    {
        PoolerObject tempPooler;

        if (sequence)
        {
            for (int i = 0; i < poolSize; i++)
            {
                for (int j = 0; j < prefabs.Length; j++)
                {
                    tempPooler = Instantiate(prefabs[j], parent);
                    allCollection.Add(tempPooler);
                    tempPooler.Init(this);
                    tempPooler.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < prefabs.Length; i++)
            {
                for (int j = 0; j < poolSize; j++)
                {
                    tempPooler = Instantiate(prefabs[i], parent);
                    allCollection.Add(tempPooler);
                    tempPooler.Init(this);
                    tempPooler.gameObject.SetActive(false);
                }
            }
        }
    }

    public virtual PoolerObject Pop(bool randomize = false)
    {
        collection.RemoveAll(obj => obj == null);
        availableCollection.Clear();
        availableCollection.AddRange(collection.ToArray());
        availableCollection.RemoveAll(obj => !obj.removeOnDisable);

        PoolerObject temp = null;

        if (availableCollection.Count > 0 && !randomize)
        {
            temp = availableCollection[0];
        }
        else if (availableCollection.Count > prefabs.Length * poolSize && randomize)
        {
            temp = availableCollection[Random.Range(0, availableCollection.Count)];
        }
        else
        {
            temp = Instantiate(prefabs[Random.Range(0, prefabs.Length)], parent);

            allCollection.Add(temp);
        }

        if (temp)
            temp.Init(this);

        if (reparent)
            temp.transform.SetParent(parent);

        return temp;
    }

    public virtual GameObject Pop(string find)
    {
        collection.RemoveAll(obj => obj == null);

        PoolerObject temp = collection.Find(x => x.name.Contains(find));

        if (temp == null)
        {
            for (int i = 0; i < prefabs.Length; i++)
            {
                if (prefabs[i].name.Contains(find))
                {
                    temp = Instantiate(prefabs[i], parent);
                    allCollection.Add(temp);
                    break;
                }
                else if (find.Contains("Clone") && find.Contains(prefabs[i].name))
                {
                    temp = Instantiate(prefabs[i], parent);
                    allCollection.Add(temp);
                    break;
                }
            }
        }

        if (temp)
            temp.Init(this);

        if (reparent)
            temp.transform.SetParent(parent);

        return temp.gameObject;
    }

    public void DisabledAllCollection()
    {
        foreach (PoolerObject _poolerObject in allCollection)
        {
            if (_poolerObject != null)
            {
                _poolerObject.gameObject.SetActive(false);

                if (reparent)
                    _poolerObject.transform.SetParent(transform);
            }
        }

        availableCollection.Clear();
        availableCollection.AddRange(collection.ToArray());
        availableCollection.RemoveAll(obj => !obj.removeOnDisable);
    }

    public virtual void Add(PoolerObject pooler)
    {
        if (!collection.Contains(pooler))
        {
            collection.Add(pooler);
        }
    }

    public virtual void Remove(PoolerObject pooler)
    {
        if (collection.Contains(pooler))
        {
            collection.Remove(pooler);
        }
    }
}