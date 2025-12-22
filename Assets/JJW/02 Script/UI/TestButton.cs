using UnityEngine;

namespace JJW._02_Script.UI
{
    public class TestButton : MonoBehaviour
    {
        [SerializeField] private IntEventChannel eventChannel;
        private int test = 0;
        private bool testbool = false;
        public void HpTest()
        {
            if (test >= 10)
            {
                testbool = true;
            }
            else if(test <=0)
            {
                testbool = false;
            }

            if (testbool == true)
            {
                test--;
                eventChannel.Raise(test);
            }
            else if(testbool == false)   
            {
                test++;
                eventChannel.Raise(test);
            }

        }
    }
}