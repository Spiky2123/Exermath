using TMPro;
using UnityEngine;

public class EcuationManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _result;
    [SerializeField] private TMP_Text _leftTerm;
    [SerializeField] private TMP_Text _rightTerm;

    private int _solution;
    private int _maxNumber = 100;

    public int Solution => _solution;

    public void NewEcuation()
    {
        int result = -1, leftTerm = -1, rightTerm = -1;

        _solution = Random.Range(1, 5);
       
        switch (_solution)
        {
            case 1:
                do
                {
                    result = Random.Range(0, _maxNumber);
                    leftTerm = Random.Range(0, result);
                    rightTerm = result - leftTerm;
                }while(result == leftTerm * rightTerm);
                break;
            case 2:
                do
                {
                    leftTerm = Random.Range(0, _maxNumber);
                    result = Random.Range(0, leftTerm);
                    rightTerm = leftTerm - result;
                }while (leftTerm == result * rightTerm);
                break;
            case 3:
                do
                {
                    result = Random.Range(2, _maxNumber);
                    leftTerm = Random.Range(2, result);
                    rightTerm = result / leftTerm;
                } while (rightTerm == 1 || result == leftTerm + rightTerm);
                result = leftTerm * rightTerm;
                break;
            case 4:
                do {
                    leftTerm = Random.Range(2, _maxNumber);
                    result = Random.Range(2, leftTerm);
                    rightTerm = leftTerm / result;
                }while(rightTerm == 1 || result == leftTerm - rightTerm);
                leftTerm = rightTerm * result;
                break;
        }

        UpdateUI(leftTerm, rightTerm, result);

        IncreaseDifficulty();
    }

    private void UpdateUI(int leftTerm, int rightTerm, int result)
    {
        _result.text = result.ToString();
        _leftTerm.text = leftTerm.ToString();
        _rightTerm.text = rightTerm.ToString();
    }

    private void IncreaseDifficulty()
    {
        if (_maxNumber < 9999)
            _maxNumber += Random.Range(10, 100);
        if (_maxNumber > 9999)
            _maxNumber = 9999;
    }

}
