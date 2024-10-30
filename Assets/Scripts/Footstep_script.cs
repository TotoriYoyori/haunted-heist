using UnityEngine;

public class Footstep_script : MonoBehaviour
{
    [SerializeField] int footstep_interval;
    [SerializeField] int footstep_lifetime;
    int footstep_time;

    [SerializeField] GameObject footstep;
    GameObject new_footstep;
    float adj = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        footstep_time = footstep_interval;
    }
    public void FootstepCreationCheck()
    {
        footstep_time--;
        if (footstep_time < 1)
        {
            new_footstep = Instantiate(footstep, transform.position, Quaternion.identity);

            new_footstep.GetComponent<Individual_footstep_script>().left_or_right = adj;
            new_footstep.GetComponent<Individual_footstep_script>().lifetime = footstep_lifetime;

            footstep_time = footstep_interval;
            adj = adj * -1;
        }
    }
}
