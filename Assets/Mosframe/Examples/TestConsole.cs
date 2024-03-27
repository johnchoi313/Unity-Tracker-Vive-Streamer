/*
 * TestConsole.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */
namespace Mosframe {

    using System.Text;
    using System.Collections;
    using UnityEngine;

    public class TestConsole : MonoBehaviour {
        private void Start () {

            StartCoroutine( this.onTest() );
        }

        public void onClickOpenConsole () {

            if( RealtimeConsole.Instance.isOpen )
                RealtimeConsole.Instance.close();
            else
                RealtimeConsole.Instance.open();
        }

        IEnumerator onTest () {

            yield return new WaitForSeconds( 1.0f );

            while( true ) {


                var sb = new StringBuilder();

                sb.Append( RichText.Bold( RichText.Color( "Test : 한글테스트 : 1234567890\nABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", new Color32( (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), 255 ).toHtmlColor() ) ) );
                sb.Append( RichText.Color( Random.Range( 1000, 10000 ), new Color32( (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), 255 ).toHtmlColor() ) ).Append( '\n' );

                sb.Append( RichText.Color( "Test Value : ", new Color32( (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), 255 ).toHtmlColor() ) );
                sb.Append( RichText.Color( Random.Range( 100000, 1000000 ), new Color32( (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), 255 ).toHtmlColor() ) ).Append( '\n' );

                var p = Random.Range(0,100);
                if( p < 30 ) {
                    Debug.LogError( sb );
                }
                else
                if( p < 60 ) {
                    Debug.LogWarning( sb );
                }
                else {
                    Debug.Log( sb );
                }

                yield return new WaitForSeconds( 0.5f );
            }
        }

    }
}